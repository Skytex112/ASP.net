using System;
using System.Linq;
using TeaShop.CQRS.Command;
using TeaShop.DTO;
using TeaShop.Models;

namespace TeaShop.Mapping
{
    public interface ISimpleMapper
    {
        Tea MapToTea(CreateTeaCommand command);
        void MapToTea(UpdateTeaCommand command, Tea tea);
        TeaDto MapToDto(Tea tea);
    }

    public class SimpleMapper : ISimpleMapper
    {
        public Tea MapToTea(CreateTeaCommand command)
        {
            return new Tea
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Type = command.Type,
                Price = command.Price,
                Quantity = command.Quantity
            };
        }

        public void MapToTea(UpdateTeaCommand command, Tea tea)
        {
            tea.Name = command.Name;
            tea.Type = command.Type;
            tea.Price = command.Price;
            tea.Quantity = command.Quantity;
        }

        public TeaDto MapToDto(Tea tea)
        {
            if (tea == null) return null;
            return new TeaDto
            {
                Id = tea.Id,
                Name = tea.Name,
                Type = tea.Type,
                Price = tea.Price,
                Quantity = tea.Quantity
            };
        }
    }
}
