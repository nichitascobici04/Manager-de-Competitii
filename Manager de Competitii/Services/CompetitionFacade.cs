using System;
using System.Collections.Generic;
using Manager_de_Competitii.Models;

namespace Manager_de_Competitii.Services
{
    /// <summary>
    /// Facade class that simplifies the process of setting up a new competition.
    /// It hides the complexity of interacting with multiple service classes.
    /// </summary>
    public class CompetitionFacade
    {
        private UserCrudService _userService;
        private CompetitionService _competitionService;
        private StageGenerationService _stageService;

        public CompetitionFacade()
        {
            _userService = new UserCrudService();
            _competitionService = new CompetitionService();
            _stageService = new StageGenerationService();
        }

        public void StartNewCompetition(int organizerId, Competition competition, List<Participant> participants, List<Match> initialMatches)
        {
            Console.WriteLine("--- Starting Competition Setup via Facade ---");
            
            // 1. Verify Organizer
            User organizer = _userService.GetUser(organizerId);
            Console.WriteLine("Organizer verified.");

            // 2. Add Participants and Send Invitations
            if (participants != null)
            {
                foreach (var participant in participants)
                {
                    _competitionService.AddParticipant(competition, participant);
                    _competitionService.SendInvitation(participant);
                }
                Console.WriteLine($"Added {participants.Count} participants and sent invitations.");
            }

            // 3. Generate initial stages
            List<MatchSet> stages = _stageService.GenerateStages(initialMatches);
            Console.WriteLine("Stages generated.");

            Console.WriteLine("--- Competition Setup Complete ---");
        }
    }
}
