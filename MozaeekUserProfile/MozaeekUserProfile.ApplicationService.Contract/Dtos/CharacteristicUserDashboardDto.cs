namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class CharacteristicUserDashboardDto
    {
        public long SelectedLabelId { get; set; }
        public long ParentLabelId { get; set; }
        public string SelectedLabelTitle { get; set; }
    }
}