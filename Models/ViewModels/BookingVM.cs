using System.ComponentModel.DataAnnotations;

namespace Wheels_Away.Models.ViewModels
{
	public class BookingVM
	{
		public int Id { get; set; }

		public string PickupLocation { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime PickupDate { get; set; }

		[DataType(DataType.Time)]
		[DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
		public DateTime PickupTime { get; set; }

		public string DropLocation { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime DropDate { get; set; }

		[DataType(DataType.Time)]
		[DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
		public DateTime DropTime { get; set; }
	}

}
