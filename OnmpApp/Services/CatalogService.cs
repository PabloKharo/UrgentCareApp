﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using OnmpApp.Database;
using OnmpApp.Helpers;
using OnmpApp.Models.Database;
using OnmpApp.Properties;

namespace OnmpApp.Services;

public static class CatalogService
{
    public static async Task<List<CatalogShort>> Search(string search, int skip = 0, int take = 0)
    {
        try
        {
            return await CatalogTable.Search(search, skip, take);
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.Error);
        }
        return null;
    }


    // Удаление карточки
    public static async Task<Catalog> Get(string name)
    {
        try
        {
            var catalog = await CatalogTable.Get(name);

            if(string.IsNullOrEmpty(catalog.Text))
            {
                using var client = new HttpClient();
                HttpResponseMessage response = new();

                if (catalog.ElType == CatalogType.Diagnose)
                {
                    response = await client.GetAsync($"{Settings.ApiAddress}/diagnoses/get_diagnoses_by_code/?code={catalog.Name}");

                    catalog.Text = await response.Content.ReadAsStringAsync();
                }
                if (catalog.ElType == CatalogType.Disease)
                {
                    response = await client.GetAsync($"{Settings.ApiAddress}/diseases/get_diseases_by_tag/");

                }
                if (catalog.ElType == CatalogType.Medicine)
                {
                    response = await client.GetAsync($"{Settings.ApiAddress}/medicines/get_medicines/?search={catalog.Name}");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic json = JObject.Parse(responseContent);

                    catalog.Text = json[catalog.Name].ToString();
                }

                await CatalogTable.Update(catalog);
            }

            return catalog;
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.Error);
        }

        return null;

    }

    public static async Task<bool> Update(Catalog catalog)
    {
        try
        {
            return await CatalogTable.Update(catalog);
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.Error);
        }

        return false;
    }

    public static async Task<bool> Insert(string name, CatalogType type, string text = null)
    {
        try
        {
            return await CatalogTable.Insert(name, type, text);
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.Error);
        }

        return false;
    }



}