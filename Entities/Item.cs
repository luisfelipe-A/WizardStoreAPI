using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WizStore.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class MagicItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }

        public int MagicPower { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        //used to keep a high priority list of items to track stock amouts
        public bool IsFavored { get; set; }

        [NotMapped]
        private const int _lowStockAmount = 3;

        public static int GetLowStockAmount()
        {
            return _lowStockAmount;
        }

        public MagicItem(string name, string? description, int quantity, int magicPower, bool isFavored)
        {
            Name = name;
            Description = description;
            Quantity = quantity;
            MagicPower = magicPower;
            IsFavored = isFavored;
        }
    }
}
