namespace EdgeWorks.Shared.Configurations.BlizzardAPIs
{
    public class ApiSettings
    {
        public string Region { get; set; }

        public string Realm { get; set; }

        public string Locale { get; set; }

        public string Static
        {
            get
            {
                return string.Format("static-{0}", Region);
            }
        }

        public string Dynamic
        {
            get
            {
                return string.Format("dynamic-{0}", Region);
            }
        }

        public string Profile
        {
            get
            {
                return string.Format("profile-{0}", Region);
            }
        }
    }
}
