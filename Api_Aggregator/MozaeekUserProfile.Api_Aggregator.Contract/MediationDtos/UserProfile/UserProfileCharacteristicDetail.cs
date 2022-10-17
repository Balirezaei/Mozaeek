using System.Collections.Generic;
using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;

namespace Api_Aggregator.Contract.MediationDtos
{
    public class UserProfileCharacteristicDetail
    {
        public string LabelParentTitle { get; set; }
        public string SelectedLabel { get; set; }
        public int UserProfileCharacteristicId { get; set; }
        public long SelectedLabelId { get; set; }
        public long FirstNodeId { get; set; }

    }
    public class UserProfileCharacteristicSelectDto
    {
        public string OwnerName { get; set; }
        public List<UserProfileCharacteristicDetail> UserProfileCharacteristicDetails { get; set; }

    }
    public class UnionUserProfileCharacteristicSelectAndUnSelectedDto
    {
        public List<UserProfileCharacteristicDetail> UserProfileCharacteristicDetails { get; set; }
        public List<LabelGrid> UnSelectedLabel { get; set; }

    }
}