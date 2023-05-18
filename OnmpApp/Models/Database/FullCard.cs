using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmpApp.Models.Database;

public class FullCard
{

    [TextQuestion("Анамнез", typeof(int))]
    public string Topic1 { get; set; }
    [RadioButtonQuestion("Общее состояние", new string[] { "удовл.", "ср. тяжести", "тяжелое", "терминальное" })]
    public string Topic2 { get; set; }
    [RadioButtonWithTextQuestion("Сознание", new string[] { "ясное", "оглушенное", "сопор", "кома" })]
    public string Topic3 { get; set; }
    [RadioButtonWithTextQuestion("Положение", new string[] { "активное", "пассивное", "вынужденное" }, "в")]
    public string Topic4 { get; set; }
    [CheckBoxWithTextQuestion("Кожные покровы", new string[] { "сухие", "влажные", "обычной окраски", "бледные", "гиперемия", "цианоз", "желтушность" })]
    public string Topic5 { get; set; }
    [RadioButtonWithTextQuestion("Сыпь", new string[] { "нет" })]
    public string Topic6 { get; set; }
    [RadioButtonWithTextQuestion("Зев", new string[] { "чистый" })]
    public string Topic7 { get; set; }
    [RadioButtonWithTextQuestion("Миндалины", new string[] { "не увеличены" })]
    public string Topic8 { get; set; }
    [RadioButtonWithTextQuestion("Лимфоузлы", new string[] { "не увеличены" })]
    public string Topic9 { get; set; }
    [RadioButtonWithTextQuestion("Пролежни", new string[] { "нет" })]
    public string Topic10 { get; set; }
    [RadioButtonWithTextQuestion("Отеки", new string[] { "нет" })]
    public string Topic11 { get; set; }


    [RadioButtonWithTextQuestion("Медикаменты", new string[] { "Sol.Analgini 500mg/ml-2ml", "Sol.Ketaroli 30mg/ml-1ml", "Sol.Drotaverini 20mg/ml-2ml", "Sol.Medopredi 30mg/ml-1ml", "Sol.Prednizoloni 30mg/ml-1ml", "Sol. Dexametazoni 4mg/ml-1ml", "Sol.Chloropyramini 20mg/ml-1ml", "Sol.Spasmalini 5ml", "Sol.Metoclopramidi 5mg/ml-2ml", "Sol.Magnesii sulfatis 250mg/ml-10ml", "Sol.Ebrantili 5mg/ml-5ml", "Sol.Betaloci 1mg / ml - 5ml", "Sol.Natrii chloridi 0, 9 % -10ml", "Sol.Natrii chloridi 0, 9 % -250ml", "Sol.Glucosae 40 % -10ml", "Sol.Beroduali 20ml", "Pulmicort susp.0, 5mg / ml - 2ml", "Tab.Paracetamoli 500mg", "Tab.Captoprili 25mg", "Tab.Moxonidini 0, 4mg", "Tab.Glicini 500mg", "Tab.Carbonii activatis 250mg", "Tab.Acidi acetylsalicylici 500 mg", "в / м в правую ягодичную область", "в / в медленно под контролем АД в течении 3 минут введено" })]

    public string Topic12 { get; set; }
    /*
        [TextQuestion("Анамнез")]
        [RadioButtonQuestion("Общее состояние", new string[] {"удовл.", "ср. тяжести", "тяжелое", "терминальное"})]
        [RadioButtonWithTextQuestion("Сознание", new string[] {"ясное", "оглушенное", "сопор", "кома"})]
        [RadioButtonWithTextQuestion("Положение", new string[] {"активное", "пассивное", "вынужденное"})]
        [CheckBoxWithTextQuestion("Кожные покровы", new string[] {"сухие", "влажные", "обычной окраски", "бледные", "гиперемия", "цианоз", "желтушность"})]
        [RadioButtonWithTextQuestion("Сыпь", new string[] {"нет"})]
        [RadioButtonWithTextQuestion("Зев", new string[] {"чистый"})]
        [RadioButtonWithTextQuestion("Миндалины", new string[] {"не увеличены"})]
        [RadioButtonWithTextQuestion("Лимфоузлы", new string[] {"не увеличены"})]
        [RadioButtonWithTextQuestion("Пролежни", new string[] {"нет"})]
        [RadioButtonWithTextQuestion("Отеки", new string[] {"нет"})]
        my_form_temp();
        [TextQuestion("ЧДД")]
        [RadioButtonQuestion("Одышка", new string[] {"эксператорная", "инспираторная", "смешанная"})]
        [RadioButtonQuestion("Патологическое дыхание", new string[] {"нет", "Чейна-Стокса", "Биотта", "Куссмауля", "брадипноэ", "гиперпноэ", "тахипноэ", "шумное дыхание"})]
        [CheckBoxWithTextQuestion("Аускультативно", new string[] {"везикулярное", "жесткое", "бронхиальное", "пуэрильное", "ослаблено", "отсутствует"}, "в")]
        [RadioButtonQuestion("Хрипы", new string[] {"сухие", "Влажные"})]
        [CheckBoxWithTextQuestion("Сухие", new string[] {"свистящие", "жужжащие"}, "в")]                                                          
        [CheckBoxWithTextQuestion("Влажные", new string[] {"мелко-", "средне-", "крупнопузырчатые"}, "в")]                                        
        [CheckBoxWithTextQuestion("", new string[] {"Крепитация", "шум трения плевры"}, "над")]                                                    
        [CheckBoxWithTextQuestion("Перкуторный звук", new string[] {"легочный", "тимпанический", "коробочный", "притупленный", "тупой"}, "над")]
        [CheckBoxQuestion("Кашель", new string[] {"сухой", "влажный", "лающий", "отсутствует"})]
        [RadioButtonWithTextQuestion("Мокрота", new string[] {"нет"})]
        [TextQuestion("Пульс")]
        [RadioButtonQuestion("", new string[] {"ритмичный", "аритмичный"})]
        [RadioButtonQuestion("наполнение", new string[] {"удовлетворительное", "слабое", "не определено"})]
        [TextQuestion("ЧСС")]
        my_form_disable("Дефицит пульса");
        [TextQuestion("АД")]
        [TextQuestion("привычное")]
        [TextQuestion("максимальное")]
        [RadioButtonQuestion("Тоны сердца", new string[] {"звучные", "приглушены", "глухие"})]
        [CheckBoxWithTextQuestion("Шум", new string[] {"систолический", "диастолический", "нет"}, "на")]                            
        [RadioButtonWithTextQuestion("проводится", new string[] {"нет"})]
        [RadioButtonQuestion("", new string[] {"Шум трения перикарда", "нет"})]
        [RadioButtonWithTextQuestion("Акцент тона", "I", "II", "на")]                                                               
        [RadioButtonQuestion("Язык", "сухой", "влажный")]
        [RadioButtonWithTextQuestion("обложен", "девиация языка влево", "девиация языка право", "расположен по средней линии", "резкий запах алкоголя изо рта", "резкий запах ацетона", "следы прикуса языка")]
        [RadioButtonQuestion("Живот форма", "правильная", "увеличен", "ассиметричен")]
        [RadioButtonWithTextQuestion("", "мягкий", "напряжен", "в")]                                                                
        [RadioButtonWithTextQuestion("", "безболезненный", "болезненный", "в")]                                                     
        [RadioButtonWithTextQuestion("Положительные симптомы", "Образцова", "Ровзинга", "Ситковского", "Ортнера", "Мерфи", "Мейо-Робсона", "Щеткина-Блюмберга", "Вааля", "отрицательные", "")]
        [RadioButtonWithTextQuestion("Перистальтика", "выслушивается", "усиленная", "не выслушивается")]
        [RadioButtonWithTextQuestion("Печень", "не увеличена", "по краю реберной дуги")]
        [RadioButtonWithTextQuestion("Селезенка", "не пальпируется")]
        [RadioButtonWithTextQuestion("Рвота (частота)", "нет")]
        [RadioButtonWithTextQuestion("Стул (консистенация, частота)", "регулярный", "оформлен", "")]
        [CheckBoxQuestion("Поведение", "спокойное", "беспокойное", "возбужден")]
        [RadioButtonWithTextQuestion("Контакт", "контактен")]
        [RadioButtonQuestion("Чувствительность", "сохранена D = S", "D > S", "D < S")]
        [RadioButtonWithTextQuestion("Речь", "внятная", "дизартрия", "афазия", "")]
        [RadioButtonQuestion("Зрачки", "OD = OS", "OD > OS", "OD < OS")]
        [RadioButtonQuestion("", "обычные", "широкие", "узкие")]
        [RadioButtonWithTextQuestion("Фотореакция", "живая")]
        [RadioButtonWithTextQuestion("Нистагм", "нет", "мелкоразмашистый", "крупноразмашистый", "влево", "вправо", "при взгляде в сторону")]
        [RadioButtonWithTextQuestion("Ассиметрия лица", "нет", "сглаженность носогубной складки", "справа", "слева")]
        [RadioButtonWithTextQuestion("Менингиальные симптомы", "ригидность затылочных мышц", "Кенига", "Брудзинского", "")]
        [TextQuestion("Очаговые симптомы")]
        [RadioButtonWithTextQuestion("Координатные пробы", "в позе Ромберга", "устойчив", "неустойчив", "пальценосовая проба", "коленнопяточная проба", "выполняет правильно", "промахивается", "справа", "слева")]
        [RadioButtonWithTextQuestion("Мочеполовая система", "дизурии нет", "дизурических расстройств нет, моча светло-желтого цвета", "дизурических расстройств не выявлено, моча желтого цвета", "следы непроизвольного мочеиспускания", "задержка мочеиспускания в течении ", "мочевой пузырь увеличен и выступает над лоном на ", "симметричен, болезнен при пальпации", "частое, болезненное мочеиспускание, моча темно-желтого цвета без примесей", "частое, безболезненное мочеиспускание, моча темно-желтого цвета без примесей, ложные порывы", "болезненное мочеиспускание, моча мутная с белыми хлопьями", "безболезненное мочеиспукание, моча мутная с белыми хлопьями", "безболезненное мочеиспускание, моча темно-красного цвета со сгустками", "безболезненное мочеиспускание, моча темно-красного цвета", "неизвестно", "мочется в памперс")]
        [RadioButtonWithTextQuestion("Симптомы поколачивания", "отрицательный с обеих сторон", "положительный", "справа", "слева")]
        [TextQuestion("Вес")]
        [TextQuestion("Рост")]
        [TextQuestion("Status localis")]
        [TextQuestion("Глюкометрия")]
        [TextQuestion("SpO<sub>2</sub>")]
        [TextQuestion("ЭКГ")]
        [RadioButtonWithTextQuestion("Оказанная помощь и ее эффект", "расспрос", "изучение архива ЭКГ", "изучение мед. документов", "физические методы охлаждения")]
        [RadioButtonWithTextQuestion("Медикаменты", "Sol.Analgini 500mg/ml-2ml", "Sol.Ketaroli 30mg/ml-1ml", "Sol.Drotaverini 20mg/ml-2ml", "Sol.Medopredi 30mg/ml-1ml", "Sol.Prednizoloni 30mg/ml-1ml", "Sol. Dexametazoni 4mg/ml-1ml", "Sol.Chloropyramini 20mg/ml-1ml", "Sol.Spasmalini 5ml", "Sol.Metoclopramidi 5mg/ml-2ml", "Sol.Magnesii sulfatis 250mg/ml-10ml", "Sol.Ebrantili 5mg/ml-5ml", "Sol.Betaloci 1mg/ml-5ml", "Sol.Natrii chloridi 0,9%-10ml", "Sol.Natrii chloridi 0,9%-250ml", "Sol.Glucosae 40%-10ml", "Sol.Beroduali 20ml", "Pulmicort susp.0,5mg/ml-2ml", "Tab.Paracetamoli 500mg", "Tab.Captoprili 25mg", "Tab.Moxonidini 0,4mg", "Tab. Glicini 500mg", "Tab.Carbonii activatis 250mg", "Tab.Acidi acetylsalicylici 500 mg", "в/м в правую ягодичную область", "в/в медленно под контролем АД в течении 3 минут введено")]
        [RadioButtonWithTextQuestion("Результат вызова", "Оставлен на месте, рекомендовано обратиться в поликлинику", "Оставлен на месте, актив в поликлинику", "Вызов бригады СМП", "Отказ от вызова бригады СМП, актив ОНМП", "Отказ от вызова бригады СМП, актив в поликлинику", "Отказ от вызова бригады СМП, рекомендовано обратиться в поликлинику")]
        [TextQuestion("Оценка эффекта")]
        [RadioButtonWithTextQuestion("Рекомендации", "Ведение дневника контроля АД", "Регулярный прием базовой антигипертензивной терапии")]
        [ButtonQuestion("Сигнальная карта", "нет")]
        my_form_rashod(array("Салфетки","Бахилы","Перчатки","Маска","Шпатель","Чехол (град)","Шприц 2,0","Шприц 5,0","Шприц 10,0","Шприц 20,0","Катетер","Пластырь","Скариф","Полоски","Пакет","Маска (неб)"))

        */
}
