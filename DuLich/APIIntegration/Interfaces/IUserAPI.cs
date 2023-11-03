﻿using Newtonsoft.Json;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;
using ViewModel.Request.NguoiDung;
using ViewModel.Request.Tour;

namespace APIIntegration.Interfaces
{
    public interface IUserAPI
    {
        [Get("users/{id}")]
        Task<NguoiDung> GetById([Path("id")] int id, [Header("Authorization")] string authorization);

        [Put("users")]
        Task EditUser([Body] HttpContent content, [Header("Authorization")] string authorization);
    }
    public static class UserAPIExtensions
    {
        public static async Task EditUser(this IUserAPI api, ChinhSuaNguoiDungRequest request, string token)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(request.Id.ToString()), "Id" },
                { new StringContent(request.Email), "Email" },
                { new StringContent(request.HoTen), "HoTen" },
                { new StringContent(request.Sdt), "Sdt" },
                { new StringContent(request.NoiO), "NoiO" },
                { new StringContent(request.GioiTinh.ToString()), "GioiTinh" },
                { new StringContent(request.CCCD), "CCCD" },
                { new StringContent(request.PhanQuyen.ToString()), "PhanQuyen" },
                { new StringContent(request.TrangThai.ToString()), "TrangThai" },
            };
            byte[] userBytes;
            string fileName = "";
            if (!string.IsNullOrEmpty(request.MatKhau))
            {
                requestContent.Add(new StringContent(request.MatKhau), "MatKhau");
            }
            if (request.AnhDaiDien != null)
            {
                using (var stream = new BinaryReader(request.AnhDaiDien.OpenReadStream()))
                {
                    userBytes = stream.ReadBytes((int)request.AnhDaiDien.Length);
                }
                fileName = request.AnhDaiDien.FileName;
                requestContent.Add(new ByteArrayContent(userBytes), "AnhDaiDien", fileName);
            }

            await api.EditUser(requestContent, token);
        }
    }
}