using FluiTec.AppFx.Data;

namespace EdgeWorks.Data.System
{
    [EntityName("FileSaveResponse")]
    public class FileSaveResponse : IEntity<int>
    {
        public int Id { get; set; }

        public string Filename { get; set; }

        public string Extension { get; set; }

        public string Hash { get; set; }

        public string Path { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }
}
