using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace OnmpApp.Models;

// Возможные типы медицинских карт
public enum CardStatus
{
    [Description("Черновик")]
    Draft,
    [Description("Готовый")]
    Ready,
    [Description("Шаблон")]
    Template,
    [Description("Архив")]
    Archive
}

// Короткое описание медицинской карты
[Table("Сards")]
public class Card
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull, Indexed(Name = "userid_idx", Order = 1)]
    public int UserId { get; set; }

    // Название карты 
    [NotNull, MaxLength(64)]
    public string Name { get; set; }

    // Дата вызова
    [NotNull]
    public DateTime Date { get; set; } = DateTime.Now;

    // Номер наряда
    [NotNull, MaxLength(64)]
    public string Order { get; set; }

    // Статус карты 
    [NotNull, MaxLength(8), Column("Status")]
    public string _status { get; set; }

    [Ignore]
    public CardStatus Status
    {
        get => (CardStatus)Enum.Parse(typeof(CardStatus), _status);
        set => _status = value.ToString();
    }

    // Комментарий 
    [MaxLength(45)]
    public string Comment { get; set; }
}

