using ProjectDB.ViewModel;

namespace ProjectDB.ViewModels
{
    public class StdCrsStudent
    {
        public int stdId { get; set; }
        public string? stdName {  get; set; }
        public List<int> ?grade { get; set; }
        public required List< string> crsName { get; set; }
        
	}
}
