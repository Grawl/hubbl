using System.Runtime.Serialization;
using PCLCore;
using PCLStorage;
using Newtonsoft.Json;
using System;

namespace Hubl.Core.Model
{
	[DataContract]
    public class User
    {
		[DataMember]
        public int Port { get; set; }

		[DataMember]
        public string IpAddress { get; set; }

		[DataMember]
        public string Title { get; set; }

		[DataMember]
        public string Id { get; set; }

		[DataMember]
		public VkUserInfo VkUserInfo { get; set;}

		[DataMember]
		public bool IsHub {get; set; }

		[DataMember]
		public SCUserInfo SoundCloud { get; set; }

		[DataMember]
		public string Hub {get; set; }

		public void Save()
		{
			var source = new System.Threading.CancellationTokenSource (10000);
			var token = source.Token;
			var file = FileSystem.Current.LocalStorage.CreateFileAsync ("user", CreationCollisionOption.ReplaceExisting, token).Result;
			var jUser = JsonConvert.SerializeObject (this);
			file.WriteAllTextAsync (jUser);
		}
		public static User LoadUser()
		{
			var source = new System.Threading.CancellationTokenSource (10000);
			var token = source.Token;
			try {
				var file = FileSystem.Current.LocalStorage.GetFileAsync ("user", token).Result;			
				var sUser = file.ReadAllTextAsync ().Result;
				var user = JsonConvert.DeserializeObject<User>(sUser);
				user.IsHub = false;
				return user;
			} catch (Exception e) {
				return null;
			}
		}
    }
}
