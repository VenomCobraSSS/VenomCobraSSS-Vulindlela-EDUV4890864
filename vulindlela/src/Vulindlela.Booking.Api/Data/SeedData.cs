public static class SeedData
{
    public static void Seed(BookingDbContext context)
    {
        // if data is found, assume seeding has already been done
        if (!context.Branches.Any())
        {
            context.Branches.AddRange(
                new Branch
                {
                    BranchCode = "JHB-CENTRAL-01",
                    BranchName = "Johannesburg Central"
                },
                new Branch
                {
                    BranchCode = "CPT-CBD-01",
                    BranchName = "Cape Town CBD"
                },
                new Branch
                {
                    BranchCode = "DBN-CENTRAL-01",
                    BranchName = "Durban Central"
                }
            );

            context.SaveChanges();
        }

        if (!context.AppointmentSlots.Any())
        {
            List<Branch> branches = context.Branches
            .Where(b =>
                b.BranchCode == "JHB-CENTRAL-01" ||
                b.BranchCode == "CPT-CBD-01" ||
                b.BranchCode == "DBN-CENTRAL-01")
            .ToList();
            List<AppointmentSlot> appointmentSlots = new List<AppointmentSlot>();
            DateTime today = DateTime.Now;
            int year = today.Year;
            int month = today.Month;
            int day = today.Day;
            foreach (var branch in branches)
            {
                DateTime addDateTime = new DateTime(year, month, day);
                for (int addDay = 0; addDay < 14; addDay++)
                {
                    DateTime slotDate = addDateTime.AddDays(addDay);
                    // I will assume for now that the slots run between 8:00 and 16:00 and are an hour long
                    for (int hour = 8; hour < 16; hour++)
                    {
                        appointmentSlots.Add(
                            new AppointmentSlot
                            {
                                BranchId = branch.Id,
                                StartTime = slotDate.AddHours(hour),
                                EndTime = slotDate.AddHours(hour + 1),
                                IsBooked = false
                            }
                        );
                    }
                }
            }
            context.AppointmentSlots.AddRange(appointmentSlots);
            context.SaveChanges();
        }
    }
}