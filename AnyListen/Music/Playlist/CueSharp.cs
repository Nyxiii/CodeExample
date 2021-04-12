
﻿/*
Title:    CueSharp
Version:  0.5
Released: March 24, 2007

Author:   Wyatt O'Day
Website:  wyday.com/cuesharp
*/
using System;
using System.Text;
using System.IO;

namespace CueSharp
{
    /// <summary>
    /// A CueSheet class used to create, open, edit, and save cuesheets.
    /// </summary>
    public class CueSheet
    {
        #region Private Variables
        string[] cueLines;

        private string m_Catalog = "";
        private string m_CDTextFile = "";
        private string[] m_Comments = new string[0];
        // strings that don't belong or were mistyped in the global part of the cue
        private string[] m_Garbage = new string[0];
        private string m_Performer = "";
        private string m_Songwriter= "";
        private string m_Title="";
        private Track[] m_Tracks = new Track[0];

        #endregion

        #region Properties


        /// <summary>
        /// Returns/Sets track in this cuefile.
        /// </summary>
        /// <param name="tracknumber">The track in this cuefile.</param>
        /// <returns>Track at the tracknumber.</returns>
        public Track this[int tracknumber]
        {
            get
            {
                return m_Tracks[tracknumber];
            }
            set
            {
                m_Tracks[tracknumber] = value;
            }
        }


        /// <summary>
        /// The catalog number must be 13 digits long and is encoded according to UPC/EAN rules.
        /// Example: CATALOG 1234567890123
        /// </summary>
        public string Catalog
        {
            get { return m_Catalog; }
            set { m_Catalog = value; }
        }

        /// <summary>
        /// This command is used to specify the name of the file that contains the encoded CD-TEXT information for the disc. This command is only used with files that were either created with the graphical CD-TEXT editor or generated automatically by the software when copying a CD-TEXT enhanced disc.
        /// </summary>
        public string CDTextFile
        {
            get { return m_CDTextFile; }
            set { m_CDTextFile = value; }
        }

        /// <summary>
        /// This command is used to put comments in your CUE SHEET file.
        /// </summary>
        public string[] Comments
        {
            get { return m_Comments; }
            set { m_Comments = value; }
        }

        /// <summary>
        /// Lines in the cue file that don't belong or have other general syntax errors.
        /// </summary>
        public string[] Garbage => m_Garbage;

        /// <summary>
        /// This command is used to specify the name of a perfomer for a CD-TEXT enhanced disc.
        /// </summary>
        public string Performer
        {
            get { return m_Performer; }
            set { m_Performer = value; }
        }

        /// <summary>
        /// This command is used to specify the name of a songwriter for a CD-TEXT enhanced disc.
        /// </summary>
        public string Songwriter
        {
            get { return m_Songwriter; }
            set { m_Songwriter = value; }
        }

        /// <summary>
        /// The title of the entire disc as a whole.
        /// </summary>
        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }

        /// <summary>
        /// The array of tracks on the cuesheet.
        /// </summary>
        public Track[] Tracks
        {
            get { return m_Tracks; }
            set { m_Tracks = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a cue sheet from scratch.
        /// </summary>
        public CueSheet()
        { }

        /// <summary>
        /// Parse a cue sheet string.
        /// </summary>
        /// <param name="cueString">A string containing the cue sheet data.</param>
        /// <param name="lineDelims">Line delimeters; set to "(char[])null" for default delimeters.</param>
        public CueSheet(string cueString, char[] lineDelims)
        {
            if (lineDelims == null)
            {
                lineDelims = new char[] { '\n' };
            }

            cueLines = cueString.Split(lineDelims);
            RemoveEmptyLines(ref cueLines);
            ParseCue(cueLines);
        }

        /// <summary>
        /// Parses a cue sheet file.
        /// </summary>
        /// <param name="cuefilename">The filename for the cue sheet to open.</param>
        public CueSheet(string cuefilename)
        {
            ReadCueSheet(cuefilename, Encoding.Default);
        }

        /// <summary>
        /// Parses a cue sheet file.
        /// </summary>
        /// <param name="cuefilename">The filename for the cue sheet to open.</param>
        /// <param name="encoding">The encoding used to open the file.</param>
        public CueSheet(string cuefilename, Encoding encoding)
        {
            ReadCueSheet(cuefilename, encoding);
        }

        public CueSheet(StreamReader file)
        {
            ReadCueSheet(file);
        }

        private void ReadCueSheet(StreamReader file)
        {
            // array of delimiters to split the sentence with
            char[] delimiters = new char[] { '\n' };

            //read in file
            cueLines = file.ReadToEnd().Split(delimiters);

            RemoveEmptyLines(ref cueLines);

            ParseCue(cueLines);
        }

        private void ReadCueSheet(string filename, Encoding encoding)
        {
            // array of delimiters to split the sentence with
            char[] delimiters = new char[] { '\n' };

            // read in the full cue file
            TextReader tr = new StreamReader(filename, encoding);
            //read in file
            cueLines = tr.ReadToEnd().Split(delimiters);

            // close the stream
            tr.Close();

            RemoveEmptyLines(ref cueLines);

            ParseCue(cueLines);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Removes any empty lines, elimating possible trouble.
        /// </summary>
        /// <param name="file"></param>
        private void RemoveEmptyLines(ref string[] file)
        {
            int itemsRemoved = 0;

            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Trim() != "")
                {
                    file[i - itemsRemoved] = file[i];
                }
                else if (file[i].Trim() == "")
                {
                    itemsRemoved++;
                }
            }

            if (itemsRemoved > 0)
            {
                file = (string[])ResizeArray(file, file.Length - itemsRemoved);
            }
        }

        private void ParseCue(string[] file)
        {
            //-1 means still global, 
            //all others are track specific
            int trackOn = -1;
            AudioFile currentFile = new AudioFile();

            for (int i = 0; i < file.Length; i++)
            {
                file[i] = file[i].Trim();

                switch (file[i].Substring(0, file[i].IndexOf(' ')).ToUpper())
                {
                case "CATALOG":
                    ParseString(file[i], trackOn);
                    break;
                case "CDTEXTFILE":
                    ParseString(file[i], trackOn);
                    break;
                case "FILE":
                    currentFile = ParseFile(file[i], trackOn);
                    break;
                case "FLAGS":
                    ParseFlags(file[i], trackOn);
                    break;
                case "INDEX":
                    ParseIndex(file[i], trackOn);
                    break;
                case "ISRC":
                    ParseString(file[i], trackOn);
                    break;
                case "PERFORMER":
                    ParseString(file[i], trackOn);
                    break;
                case "POSTGAP":
                    ParseIndex(file[i], trackOn);
                    break;
                case "PREGAP":
                    ParseIndex(file[i], trackOn);
                    break;
                case "REM":
                    ParseComment(file[i], trackOn);
                    break;
                case "SONGWRITER":
                    ParseString(file[i], trackOn);
                    break;
                case "TITLE":
                    ParseString(file[i], trackOn);
                    break;
                case "TRACK":
                    trackOn++;
                    ParseTrack(file[i], trackOn);
                    if (currentFile.Filename != "") //if there's a file
                    {
                        m_Tracks[trackOn].DataFile = currentFile;
                        currentFile = new AudioFile();
                    }
                    break;
                default:
                    ParseGarbage(file[i], trackOn);
                    //save discarded junk and place string[] with track it was found in
                    break;
                }
            }

        }

        private void ParseComment(string line, int trackOn)
        {
            //remove "REM" (we know the line has already been .Trim()'ed)
            line = line.Substring(line.IndexOf(' '), line.Length - line.IndexOf(' ')).Trim();

            if (trackOn == -1)
            {
                if (line.Trim() != "")
                {
                    m_Comments = (string[])ResizeArray(m_Comments, m_Comments.Length + 1);
                    m_Comments[m_Comments.Length - 1] = line;
                }
            }
            else
            {
                m_Tracks[trackOn].AddComment(line);
            }
        }

        private AudioFile ParseFile(string line, int trackOn)
        {
            string fileType;

            line = line.Substring(line.IndexOf(' '), line.Length - line.IndexOf(' ')).Trim();

            fileType = line.Substring(line.LastIndexOf(' '), line.Length - line.LastIndexOf(' ')).Trim();

            line = line.Substring(0, line.LastIndexOf(' ')).Trim();

            //if quotes around it, remove them.
            if (line[0] == '"')
            {
                line = line.Substring(1, line.LastIndexOf('"') - 1);
            }

            return new AudioFile(line, fileType);
        }

        private void ParseFlags(string line, int trackOn)
        {
            string temp;

            if (trackOn != -1)
            {
                line = line.Trim();
                if (line != "")
                {
                    try
                    {
                        temp = line.Substring(0, line.IndexOf(' ')).ToUpper();
                    }
                    catch (Exception)
                    {
                        temp = line.ToUpper();

                    }

                    switch (temp)
                    {
                    case "FLAGS":
                        m_Tracks[trackOn].AddFlag(temp);
                        break;
                    case "DATA":
                        m_Tracks[trackOn].AddFlag(temp);
                        break;
                    case "DCP":
                        m_Tracks[trackOn].AddFlag(temp);
                        break;
                    case "4CH":
                        m_Tracks[trackOn].AddFlag(temp);
                        break;
                    case "PRE":
                        m_Tracks[trackOn].AddFlag(temp);
                        break;
                    case "SCMS":
                        m_Tracks[trackOn].AddFlag(temp);
                        break;
                    default:
                        break;
                    }

                    //processing for a case when there isn't any more spaces
                    //i.e. avoiding the "index cannot be less than zero" error
                    //when calling line.IndexOf(' ')
                    try
                    {
                        temp = line.Substring(line.IndexOf(' '), line.Length - line.IndexOf(' '));
                    }
                    catch (Exception)
                    {
                        temp = line.Substring(0, line.Length);
                    }

                    //if the flag hasn't already been processed
                    if (temp.ToUpper().Trim() != line.ToUpper().Trim())
                    {
                        ParseFlags(temp, trackOn);
                    }
                }
            }
        }

        private void ParseGarbage(string line, int trackOn)
        {
            if (trackOn == -1)
            {
                if (line.Trim() != "")
                {
                    m_Garbage = (string[])ResizeArray(m_Garbage, m_Garbage.Length + 1);
                    m_Garbage[m_Garbage.Length - 1] = line;
                }
            }
            else
            {
                m_Tracks[trackOn].AddGarbage(line);
            }
        }

        private void ParseIndex(string line, int trackOn)
        {
            string indexType;
            string tempString;

            int number = 0;
            int minutes;
            int seconds;
            int frames;

            indexType = line.Substring(0, line.IndexOf(' ')).ToUpper();

            tempString = line.Substring(line.IndexOf(' '), line.Length - line.IndexOf(' ')).Trim();

            if (indexType == "INDEX")
            {
                //read the index number
                number = Convert.ToInt32(tempString.Substring(0, tempString.IndexOf(' ')));
                tempString = tempString.Substring(tempString.IndexOf(' '), tempString.Length - tempString.IndexOf(' ')).Trim();
            }

            //extract the minutes, seconds, and frames
            minutes = Convert.ToInt32(tempString.Substring(0, tempString.IndexOf(':')));
            seconds = Convert.ToInt32(tempString.Substring(tempString.IndexOf(':') + 1, tempString.LastIndexOf(':') - tempString.IndexOf(':') - 1));
            frames = Convert.ToInt32(tempString.Substring(tempString.LastIndexOf(':') + 1, tempString.Length - tempString.LastIndexOf(':') - 1));

            if (indexType == "INDEX")
            {
                m_Tracks[trackOn].AddIndex(number, minutes, seconds, frames);
            }
            else if (indexType == "PREGAP")
            {
                m_Tracks[trackOn].PreGap = new Index(0, minutes, seconds, frames);
            }
            else if (indexType == "POSTGAP")
            {
                m_Tracks[trackOn].PostGap = new Index(0, minutes, seconds, frames);
            }
        }