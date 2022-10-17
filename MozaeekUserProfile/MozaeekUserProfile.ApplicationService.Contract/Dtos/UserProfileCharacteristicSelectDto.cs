using System.Collections.Generic;

namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserProfileCharacteristicSelectDto
    {
        public string OwnerName { get; set; }
        public List<UserProfileCharacteristicDetail> UserProfileCharacteristicDetails { get; set; }

    }

    public class UserProfileCharacteristicDetail
    {
        public UserProfileCharacteristicDetail()
        {
        }

        public UserProfileCharacteristicDetail(string labelParentTitle, string selectedLabel, int userProfileCharacteristicId, long selectedLabelId, long firstNodeId)
        {
            LabelParentTitle = labelParentTitle;
            SelectedLabel = selectedLabel;
            UserProfileCharacteristicId = userProfileCharacteristicId;
            SelectedLabelId = selectedLabelId;
            FirstNodeId = firstNodeId;
        }

        public string LabelParentTitle { get; set; }
        public string SelectedLabel { get; set; }
        public int UserProfileCharacteristicId { get; set; }
        public long SelectedLabelId { get; set; }
        public long FirstNodeId { get; set; }

    }
}