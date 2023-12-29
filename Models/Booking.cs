using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wheels_Away.Models
{
	public class Booking
	{
		[Key]
		public int Id { get; set; }
		public string PickupLocation { get; set; }
		[DataType(DataType.Date)]
		[Column(TypeName = "date")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime PickupDate { get; set; }

		[DataType(DataType.Time)]
		[DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
		public DateTime PickupTime { get; set; }
		public string DropLocation { get; set; }


		[Column(TypeName = "date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime DropDate { get; set; }


		[DataType(DataType.Time)]
		[DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
		public DateTime DropTime { get; set; }
		[ForeignKey(nameof(UserId))]
		public string? UserId { get; set; }
		public ApplicationUser? User { get; set; }
		[ForeignKey(nameof(CarId))]
		public int? CarId { get; set; }
		public Car? Car { get; set; }

	}
}