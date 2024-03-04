namespace SeleniumAutotest.ViewModels
{
    public class DriverManagerViewModel
    {
        public List<DriverInfo> Drivers { get; set; }
    }

    public class DriverInfo
    {
        public string Name { get; set; }
        public string File { get; set; }
        public bool Exists { get; set; }
    }
}
