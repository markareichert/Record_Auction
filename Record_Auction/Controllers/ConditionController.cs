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
    public class ConditionController : Controller
    {
        private ApplicationDbContext context;

        public ConditionController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<RecordCondition> conditionList = context.RecordConditions.ToList();
            return View(conditionList);
        }

        public IActionResult Add()
        {
            AddConditionViewModel addConditionViewModel = new AddConditionViewModel();
            return View(addConditionViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddConditionViewModel addConditionViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new record condition to my existing conditions

                RecordCondition newCondition = new RecordCondition
                {
                    Name = addConditionViewModel.Name
                };

                context.RecordConditions.Add(newCondition);
                context.SaveChanges();

                return Redirect("/Condition");
            }

            return View(addConditionViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Conditions";
            ViewBag.conditions = context.RecordConditions.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] conditionIds)
        {
            foreach (int conditionId in conditionIds)
            {
                RecordCondition theCondition = context.RecordConditions.Single(c => c.ID == conditionId);
                context.RecordConditions.Remove(theCondition);
            }

            context.SaveChanges();

            return Redirect("/");
        }
    }
}
