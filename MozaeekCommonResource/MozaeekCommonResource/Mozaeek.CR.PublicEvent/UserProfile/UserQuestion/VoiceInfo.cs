namespace Mozaeek.CR.PublicEvent.UserProfile
{
    public class VoiceInfo
    {
        public string VoiceHttpPath { get; set; }
        public string VoiceFileId { get; set; }

        public VoiceInfo(string voiceHttpPath, string voiceFileId)
        {
            VoiceHttpPath = voiceHttpPath;
            VoiceFileId = voiceFileId;
        }
    }
}