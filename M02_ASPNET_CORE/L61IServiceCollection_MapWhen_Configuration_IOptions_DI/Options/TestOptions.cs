namespace L61IServiceCollection_MapWhen_Configuration_IOptions_DI.Options
{
    // Tao class co cac truong du lieu tuong ung voi configuration
    // trong appsetting.json de co the chuyen thanh doi tuong
    public class TestOptions
    {
        // Ten truong, kieu du lieu phai giong voi configuration
        public string opt_key1 { set; get; }
        public SubTestOptions opt_key2 { set; get; }
    }

    public class SubTestOptions
    {
        public string k1 { set; get; }
        public string k2 { set; get; }
    }
}


