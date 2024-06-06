using AppEase.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.IO;

namespace AppEase.Service
{

    public class GeneratePdfService
    {
        public static byte[] getCoverLetterPdf( Job job, Profile profile)
        {
            using var stream = new MemoryStream();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header()
                        .AlignCenter()
                        .Text("Cover Letter")
                        .FontSize(20)
                        .Bold();

                    page.Content()
                        .Column(column =>
                        {
                            column.Spacing(5);

                            // Applicant's Address
                            column.Item().Text(text =>
                            {
                                text.Span(profile.Name+"\n").Bold();
                                text.Span("Your Address Line 1\n");
                                text.Span("Your Address Line 2\n");
                                text.Span("City, State, ZIP Code\n");
                                text.Span("Email: "+profile.Email+"\n");
                                text.Span("Phone: (123) 456-7890\n");
                                text.Span($"Date: {DateTime.Now.ToString("MMMM dd, yyyy")}\n");
                            });

                            // Spacer
                            column.Item().Text("\n");

                            // Recipient's Address
                            column.Item().Text(text =>
                            {
                                text.Span("Hiring Manager\n").Bold();
                                text.Span(job.CompanyName+"\n");
                                text.Span("Company Address Line 1\n");
                                text.Span("Company Address Line 2\n");
                                text.Span("City, State, ZIP Code\n");
                            });

                            // Spacer
                            column.Item().Text("\n");

                            // Greeting
                            column.Item().Text("Dear Hiring Manager,\n\n");

                            // Body
                            column.Item().Text("I am writing to express my interest in the "+job.Title+" position at "+job.CompanyName+". With my background in [relevant field or experience], I am confident in my ability to contribute effectively to your team.\n\n" +
                                "I have [mention some of your key qualifications and achievements that are relevant to the job]. My experience at [previous company or educational institution] has equipped me with [mention skills or knowledge]. I am particularly drawn to [Company Name] because [mention something you appreciate about the company or the role].\n\n" +
                                "I am eager to bring my [mention specific skills or attributes] to [Company Name] and contribute to [specific goals or projects the company is known for]. Thank you for considering my application. I look forward to the opportunity to discuss how my background, skills, and certifications will be a perfect fit for this role.\n\n" +
                                "Sincerely,\n\n" +
                                profile.Name);

                            // Spacer
                            column.Item().Text("\n");

                            // Footer
                            column.Item().AlignCenter().Text("Thank you for your time and consideration.")
                                .FontSize(12)
                                .Italic();
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated using AppEase").FontSize(10);
                        });
                });

            }).GeneratePdf(stream);
            return stream.ToArray();


        }
        public static byte[] getCoverLetterPdf()
        {
            using var stream = new MemoryStream();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header()
                        .AlignCenter()
                        .Text("Cover Letter")
                        .FontSize(20)
                        .Bold();

                    page.Content()
                        .Column(column =>
                        {
                            column.Spacing(5);

                            // Applicant's Address
                            column.Item().Text(text =>
                            {
                                text.Span("Your Name\n").Bold();
                                text.Span("Your Address Line 1\n");
                                text.Span("Your Address Line 2\n");
                                text.Span("City, State, ZIP Code\n");
                                text.Span("Email: your.email@example.com\n");
                                text.Span("Phone: (123) 456-7890\n");
                                text.Span($"Date: {DateTime.Now.ToString("MMMM dd, yyyy")}\n");
                            });

                            // Spacer
                            column.Item().Text("\n");

                            // Recipient's Address
                            column.Item().Text(text =>
                            {
                                text.Span("Recipient Name\n").Bold();
                                text.Span("Company Name\n");
                                text.Span("Company Address Line 1\n");
                                text.Span("Company Address Line 2\n");
                                text.Span("City, State, ZIP Code\n");
                            });

                            // Spacer
                            column.Item().Text("\n");

                            // Greeting
                            column.Item().Text("Dear [Recipient's Name],\n\n");

                            // Body
                            column.Item().Text("I am writing to express my interest in the [Job Title] position at [Company Name]. With my background in [relevant field or experience], I am confident in my ability to contribute effectively to your team.\n\n" +
                                "I have [mention some of your key qualifications and achievements that are relevant to the job]. My experience at [previous company or educational institution] has equipped me with [mention skills or knowledge]. I am particularly drawn to [Company Name] because [mention something you appreciate about the company or the role].\n\n" +
                                "I am eager to bring my [mention specific skills or attributes] to [Company Name] and contribute to [specific goals or projects the company is known for]. Thank you for considering my application. I look forward to the opportunity to discuss how my background, skills, and certifications will be a perfect fit for this role.\n\n" +
                                "Sincerely,\n\n" +
                                "Your Name");

                            // Spacer
                            column.Item().Text("\n");

                            // Footer
                            column.Item().AlignCenter().Text("Thank you for your time and consideration.")
                                .FontSize(12)
                                .Italic();
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated using AppEase").FontSize(10);
                        });
                });

            }).GeneratePdf(stream);
            return stream.ToArray();


        }
        public static void getResumePdf()
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header()
                        .AlignCenter()
                        .Text("Your Name")
                        .FontSize(24)
                        .Bold();

                    page.Content()
                        .Column(column =>
                        {
                            column.Spacing(10);

                            // Contact Information
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(column =>
                                {
                                    column.Item().Text("Email: your.email@example.com").FontSize(12);
                                    column.Item().Text("Phone: (123) 456-7890").FontSize(12);
                                });
                                row.RelativeItem().AlignRight().Column(column =>
                                {
                                    column.Item().Text("LinkedIn: linkedin.com/in/yourprofile").FontSize(12);
                                    column.Item().Text("GitHub: github.com/yourprofile").FontSize(12);
                                });
                            });

                            // Summary
                            column.Item().Text("Summary")
                                .FontSize(16)
                                .Bold();
                            column.Item().Text("Experienced professional with a strong background in [relevant field]. Proven track record of success in [mention key achievements or skills]. Seeking to leverage expertise in [specific area] to contribute to [Company Name].")
                                .FontSize(12);

                            // Experience
                            column.Item().Text("Experience")
                                .FontSize(16)
                                .Bold();
                            column.Item().Column(experienceColumn =>
                            {
                                experienceColumn.Spacing(5);

                                experienceColumn.Item().Text("Job Title at Company Name")
                                    .Bold();
                                experienceColumn.Item().Text("Location | Dates of Employment")
                                    .Italic();
                                experienceColumn.Item().Text("• Key responsibility or achievement.")
                                    .FontSize(12);
                                experienceColumn.Item().Text("• Key responsibility or achievement.")
                                    .FontSize(12);

                                experienceColumn.Item().Text("Job Title at Previous Company")
                                    .Bold();
                                experienceColumn.Item().Text("Location | Dates of Employment")
                                    .Italic();
                                experienceColumn.Item().Text("• Key responsibility or achievement.")
                                    .FontSize(12);
                                experienceColumn.Item().Text("• Key responsibility or achievement.")
                                    .FontSize(12);
                            });

                            // Education
                            column.Item().Text("Education")
                                .FontSize(16)
                                .Bold();
                            column.Item().Column(educationColumn =>
                            {
                                educationColumn.Spacing(5);

                                educationColumn.Item().Text("Degree at University Name")
                                    .Bold();
                                educationColumn.Item().Text("Location | Graduation Date")
                                    .Italic();
                                educationColumn.Item().Text("• Relevant coursework or achievement.")
                                    .FontSize(12);
                            });

                            // Skills
                            column.Item().Text("Skills")
                                .FontSize(16)
                                .Bold();
                            column.Item().Column(skillsColumn =>
                            {
                                skillsColumn.Spacing(5);

                                skillsColumn.Item().Text("• Skill 1").FontSize(12);
                                skillsColumn.Item().Text("• Skill 2").FontSize(12);
                                skillsColumn.Item().Text("• Skill 3").FontSize(12);
                                skillsColumn.Item().Text("• Skill 4").FontSize(12);
                            });

                            // Additional Sections (e.g., Certifications, Projects)
                            column.Item().Text("Certifications")
                                .FontSize(16)
                                .Bold();
                            column.Item().Column(certificationsColumn =>
                            {
                                certificationsColumn.Spacing(5);

                                certificationsColumn.Item().Text("• Certification Name").FontSize(12);
                                certificationsColumn.Item().Text("• Certification Name").FontSize(12);
                            });

                            column.Item().Text("Projects")
                                .FontSize(16)
                                .Bold();
                            column.Item().Column(projectsColumn =>
                            {
                                projectsColumn.Spacing(5);

                                projectsColumn.Item().Text("Project Name")
                                    .Bold();
                                projectsColumn.Item().Text("• Description of the project and your role in it.").FontSize(12);
                            });
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated using AppEase").FontSize(10);
                        });
                });

            }).ShowInPreviewer();
        }

    }
}
