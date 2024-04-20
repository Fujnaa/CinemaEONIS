﻿namespace CinemaBackend.Models.DTOs.WorkerDTOs
{
    public class WorkerCreateDto
    {

        public string WorkerName { get; set; } = null!;

        public string WorkerEmailAdress { get; set; } = null!;

        public string WorkerPhoneNumber { get; set; } = null!;

        public decimal? WorkerSalary { get; set; }

        public string? WorkerCity { get; set; }

    }
}
