﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Record_Auction.Models;
using Record_Auction.Models.RecordViewModels;
using Record_Auction.Models.RecordModels;
using Record_Auction.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Record_Auction.Controllers
{
    public class RecordController : Controller
    {
        private ApplicationDbContext context;

        public RecordController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Record> records = context.Records.Include(c => c.Condition).ToList();

            return View(records);
        }

        public IActionResult Add()
        {
            AddRecordViewModel addRecordViewModel = new AddRecordViewModel(context.RecordConditions.ToList());
            return View(addRecordViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddRecordViewModel addRecordViewModel)
        {
            if (ModelState.IsValid)
            {
                RecordCondition newRecordCondition = context.RecordConditions.Single(c => c.ID == addRecordViewModel.ConditionID);

                // Add the new record to my existing record
                Record newRecord = new Record
                {
                    Artist = addRecordViewModel.Artist,
                    SideA = addRecordViewModel.SideA,
                    SideB = addRecordViewModel.SideB,
                    Notes = addRecordViewModel.Notes,
                    Label = addRecordViewModel.Label,
                    RecordNumber = addRecordViewModel.RecordNumber,
                    ConditionID = addRecordViewModel.ConditionID,
                    Condition = newRecordCondition
                };

                context.Records.Add(newRecord);
                context.SaveChanges();

                return Redirect("/Record");
            }

            return View(addRecordViewModel);
        }

        public IActionResult Remove()
        {
            //            ViewBag.title = "Remove Records";
            //            ViewBag.records = context.Records.ToList();
            IList<Record> records = context.Records.ToList();

            return View(records);
            //           return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] recordIds)
        {
            foreach (int recordId in recordIds)
            {
                Record theRecord = context.Records.Single(c => c.ID == recordId);
                context.Records.Remove(theRecord);
            }

            context.SaveChanges();

            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Auction(int[] recordIds)
        {
            return RedirectToAction("Add", "Auction", recordIds);
        }
    }
}
