namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserProfileCharacteristicCreateResult
    {
        public int Id { get; set; }
        public string OwnerTitle { get; set; }

        public string FirstLabelTitle { get; set; }

        public string SelectedLabelTitle { get; set; }

        public int UserProfileCharacteristicId { get; set; }

        public string SelectedLabelId { get; set; }

        public string FirstNodeId { get; set; }
}
}