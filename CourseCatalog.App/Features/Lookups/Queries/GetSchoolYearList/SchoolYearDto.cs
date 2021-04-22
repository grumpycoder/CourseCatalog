namespace CourseCatalog.App.Features.Lookups.Queries.GetSchoolYearList
{
    public class SchoolYearDto
    {
        public string DisplayYear => $"{Year - 1}-{Year}";
        public int Year { get; set; }
        public bool IsCurrent { get; set; }
    }
}