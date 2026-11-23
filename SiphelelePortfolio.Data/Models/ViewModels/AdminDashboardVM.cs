namespace SiphelelePortfolio.Data.Models.ViewModels;

public class AdminDashboardVM
{
    public int UnreadCollaborations { get; set; }
    public int TotalEngagements { get; set; }
    public int TotalCollaborations { get; set; }
    public List<string> ChartLabels { get; set; } = new();
    public List<int> EngagementData { get; set; } = new();
    public List<int> CollaborationData { get; set; } = new();
}