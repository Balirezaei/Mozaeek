using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Mozaeek.CR.PublicDto.Dto;

namespace MozaeekCore.NotificationManagementConsistency
{
    public class PushNotificationService
    {
        private readonly string _connectionString;

        public PushNotificationService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SendNotif(PushNotificationMessage pushNotification)
        {
            var commandText = "INSERT INTO[dbo].[PushNotification] ([CreationDate],[Title],[Content],[JsonParams],[DeliveryStatus],[DeviceId],[NotificationType],[ApplicationId]) VALUES(@CreationDate,@Title ,@Content, @JsonParams, @DeliveryStatus,@DeviceId, @NotificationType,@ApplicationId)";

            var p1 = new SqlParameter("@CreationDate", SqlDbType.DateTime2);
            p1.Value = pushNotification.PublishDateTime;

            var p2 = new SqlParameter("@Title", SqlDbType.NVarChar);
            p2.Value = pushNotification.Title;

            var p3 = new SqlParameter("@Content", SqlDbType.NVarChar);
            p3.Value = pushNotification.Content;

            var p4 = new SqlParameter("@JsonParams", SqlDbType.NVarChar);
            p4.Value = pushNotification.Payload;

            var p5 = new SqlParameter("@DeliveryStatus", SqlDbType.Int);
            p5.Value = 0;

            var p6 = new SqlParameter("@DeviceId", SqlDbType.NVarChar);
            p6.Value = pushNotification.ReceiverId;


            var p7 = new SqlParameter("@NotificationType", SqlDbType.Int);
            p7.Value = (int)pushNotification.Type;
            var p8 = new SqlParameter("@ApplicationId", SqlDbType.Int);
            p8.Value = 1;

            SqlParameter[] parameters = new[]
            {
                p1,
                p2,
                p3,
                p4,
                p5,
                p6,
                p7,
                p8
            };
            //SqlParameter parameterCredits = new SqlParameter("@Credits", creditsLow);
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

    }
}