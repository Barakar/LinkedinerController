using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Linkediner.Constans;
using Linkediner.Extensions;
using Linkediner.Interfaces;
using Linkediner.Models;

namespace Linkediner.HtmlHandlers
{
    public class LinkedinParser : ILinkedinParser
    {
        public LinkedinProfile ParseProfile(string id, HtmlDocument document)
        {
            var personName = document.GetElementbyId(LinkedinSelectorsConstans.NameSelector).InnerText;
            var title = document.GetQueryString(LinkedinSelectorsConstans.TitleSelector);
            var experiencesHtml = document.QuerySelectorAll(LinkedinSelectorsConstans.ExperienceSelector);
            var experiences = ParseExperiences(experiencesHtml);

            var currentExperience = experiences.FirstOrDefault();
            var currentPosition = currentExperience == null ? string.Empty : currentExperience.Position;

            var summary = document.GetQueryString(LinkedinSelectorsConstans.SummarySelector);
            var skills = document.QuerySelectorAll(LinkedinSelectorsConstans.SkillsSelector).Select(x => x.InnerText).ToList();

            var educationHtml = document.QuerySelectorAll(LinkedinSelectorsConstans.SchoolsSelector);
            var educations = ParseEducations(educationHtml);

            return new LinkedinProfile(id, personName, title, currentPosition, summary, skills, experiences, educations);
        }

        private List<Experience> ParseExperiences(IEnumerable<HtmlNode> htmlExperiences)
        {
            var experiences = new List<Experience>();

            foreach (var experience in htmlExperiences)
            {
                var duration = experience.QuerySelectorAll(ExperienceSelectorsConstans.TimeSelector);
                var node = new Experience
                {
                    Position = experience.GetQueryString(ExperienceSelectorsConstans.PositionSelector),
                    Description = experience.GetQueryString(ExperienceSelectorsConstans.DescriptionSelector),
                    CompanyName = experience.GetQueryString(ExperienceSelectorsConstans.CompanySelector),
                    StartTime = DateTime.Parse(duration.First().InnerText),
                    EndTime = duration.Count > 1 ? DateTime.Parse(duration.Last().InnerText) : DateTime.Now
                };

                experiences.Add(node);
            }

            //linkedin can change the order?
            return experiences.OrderByDescending(x => x.StartTime).ToList();
        }

        private List<School> ParseEducations(IEnumerable<HtmlNode> educationsHtml)
        {
            var educations = new List<School>();

            foreach (var education in educationsHtml)
            {
                var node = new School
                {
                    Title = education.GetQueryString(SchoolSelectorsConstans.TitleSelector),
                    DegreeName = education.GetQueryString(SchoolSelectorsConstans.DegreeName),
                    Summary = education.GetQueryString(SchoolSelectorsConstans.SummarySelector),
                };

                educations.Add(node);
            }

            return educations;
        }
    }
}