using AnyListen.Designer.Data;
using AnyListen.ViewModelBase;

namespace AnyListen.Designer
{
    public class ThemePackViewModel : PropertyChangedBase
    {
        #region "Singleton & Constructor"

        private static ThemePackViewModel _instance;
        public static ThemePackViewModel Instance => _inst