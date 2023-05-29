using SQLite;
using OnmpApp.Models;
using OnmpApp.Properties;
using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using OnmpApp.Models.Database;

namespace OnmpApp.Database;

public static class BaseDatabase
{
    public static SQLiteAsyncConnection DB;

    public static async Task Init()
    {
        if (DB is not null)
            return;

        DB = new SQLiteAsyncConnection(Settings.DatabasePath, Settings.DatabaseFlags);

        // Создание таблиц, если они не существуют
        _ = await DB.CreateTableAsync<User>();
        _ = await DB.CreateTableAsync<Card>();
        _ = await DB.CreateTableAsync<FullCard>();
        _ = await DB.CreateTableAsync<Catalog>();

        // TODO: Удалить
        if (await DB.Table<Catalog>().CountAsync() == 0)
        {
            List<Catalog> l = new List<Catalog>
            {
                new Catalog() { Id = 0, Name = "I10-I15.2", ElType=CatalogType.Diagnose, Text = @"{
                        ""Гипертоническая болезнь (вне криза)"": {
                             ""omps"": [
                                ""ЭКГ""
                            ],
                            ""tactics"": [
                                ""1. Рекомендовать обратиться в поликлинику.""
                            ],
                            ""sub_diagnoses"": {
                                ""При повышении САД не более чем на 20 мм. рт. ст. от привычного"": {
                                    ""sub_diag_omps"": [
                                        ""Не требует антигипертензивной терапии на этапе оказания неотложной медицинской помощи""
                                    ],
                                    ""sub_diag_recommendation"": ""Рекомендаций нет""
                                },
                                ""При повышении САД более чем на 20 мм. рт. ст. от привычного"": {
                                    ""sub_diag_omps"": [
                                        ""Контроль АД через 20 минут после терапии."",
                                        ""Каптоприл 12,5 - 25 мг или Моксонидин 0,4 мг сублингвально""
                                    ],
                                    ""sub_diag_recommendation"": ""Рекомендаций нет""
                                }
                            },
                            ""diagnose_recommendation"": ""Контроль уровня АД; Прием назначенной антигипертензивной терапии""
                        },
                        ""Гипертонический криз неосложненный"": {
                            ""omps"": [
                                ""Контроль АД во время введения препарата и через 20 минут после терапии."",
                                ""Снижение АД выполнять постепенно, не более 20%: - Моксонидин 0,4 мг или Каптоприл 12,5 - 25 мг сублингвально или же при отсутствии эффекта: - Урапидил 12,5 - 25 мг в/венно медленно в течение 5 мин."",
                                ""ЭКГ""
                            ],
                            ""tactics"": [
                                ""2. Вызов бригады СМП при отсутствии эффекта от проведённой терапии или при наличии в анамнезе аневризмы сосудов головного мозга в сочетании с головной болью."",
                                ""3. В случае отказа от вызова бригады СМП актив в ОНМПВиДН."",
                                ""1. Актив в поликлинику.""
                            ],
                            ""sub_diagnoses"": {
                                ""При тахикардии (ЧСС > 100 в минуту)"": {
                                    ""sub_diag_omps"": [
                                        ""Метопролол 12,5 - 25 мг внутрь""
                                    ],
                                    ""sub_diag_recommendation"": ""Рекомендаций нет""
                                },
                                ""При хронической почечной недостаточности"": {
                                    ""sub_diag_omps"": [
                                        ""Моксонидин 0,4 мг сублингвально""
                                    ],
                                    ""sub_diag_recommendation"": ""Противопоказаны: ингибиторы АПФ и мочегонные""
                                }
                            },
                            ""diagnose_recommendation"": ""Рекомендаций нет""
                        }
                    }"
                },
                new Catalog() { Id = 1, Name = "Ангина", ElType=CatalogType.Disease, Text = @"{
                    ""tag"": ""Инфекционные заболевания"",
                    ""description"": ""Ангина (angina pharyngis; синоним: острый тонзиллит, острый амигдалит) — это общее острое инфекционное заболевание, при котором воспалительные явления выражены главным образом в лимфаденоидной ткани глотки (чаще — миндалин).Острое воспаление лимфатического глоточного кольца, чаще небных миндалин, вызвано стрептококками, стафилококками или другими микроорганизмами."",
                    ""period"": null,
                    ""symptomps"": [
                        ""Головная боль."",
                        ""Т 38-39°С, озноб, потливость."",
                        ""В зеве разлитая гиперемия и инфильтрация мягкого неба и дужек."",
                        ""Слабость."",
                        ""Подчелюстные лимфоузлы увеличены, болезненные."",
                        ""Отека зева и подкожной клетчатки нет."",
                        ""Дужки миндалин контурируются."",
                        ""Боли в мышцах и суставах."",
                        ""Налет на миндалинах легко снимается шпателем, без повреждения эпителиального слоя."",
                        ""Миндалины увеличены, на их поверхности желто-белые фолликулы, одинаковые (1-3 мм), правильной формы, не выходящие за пределы свободной поверхности миндалин."",
                        ""Боль в горле при глотании.""
                    ],
                    ""forms"": [
                        null
                    ],
                    ""form descriptions"": [
                        null
                    ],
                    ""form symptomps"": [
                        null
                    ]
                    }" 
                },
                new Catalog(){Id = 2, Name = "S. Aminophyllinum 24 mg/ml", ElType=CatalogType.Medicine, Text=@"{
                    ""genitive"": ""S. Aminophyllini 24 mg/ml"",
                    ""unit"": ""mg"",
                    ""diagnoses"": {
                      ""Общая дозировка"": {
                        ""adult_dosage"": 240,
                        ""child_dosage"": 4
                      }
                    },
                    ""contraindications"": [
                      ""Гиперчувствительность"",
                      ""Острый инфаркт миокарда"",
                      ""Первая половина беременности""
                    ],
                    ""child_dosage_unit"": ""weight""
                    }"
                }

            };

            await DB.InsertAllAsync(l);
        }
    }

}

