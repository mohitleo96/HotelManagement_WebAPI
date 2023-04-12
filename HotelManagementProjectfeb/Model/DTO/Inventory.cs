using System.ComponentModel.DataAnnotations;

namespace HotelManagementProjectfeb.Model.DTO
{
    public class Inventory
    {
        [Key]
        public Guid Inventory_Id { get; set; }

        public string Inventory_Name { get; set; }

        public int quantity { get; set; }
    }
}
