using JaysAi.Finale.AI;

namespace JaysAi.Finale.AI
{
    public class TargetProfile
    {
        public int EnemyId { get; }
        public BoneTarget PreferredBone { get; set; } = BoneTarget.Chest;
        public float AimSmoothing { get; set; } = 0.5f;
        public float SnapDelayMs { get; set; } = 0f;
        public bool EnablePrediction { get; set; } = true;
        public float ConfidenceScore { get; set; } = 0f;

        public TargetProfile(int id)
        {
            EnemyId = id;
        }

        public bool ShouldSnapNow(float deltaTime)
        {
            // Example logic — allow snap if delay has passed
            return deltaTime >= SnapDelayMs / 1000f;
        }

        public void UpdateConfidence(float snapSuccessRate)
        {
            ConfidenceScore = snapSuccessRate;
            AdjustStrategyBasedOnConfidence();
        }

        private void AdjustStrategyBasedOnConfidence()
        {
            if (ConfidenceScore > 0.8f)
            {
                PreferredBone = BoneTarget.Head;
                AimSmoothing = 0.3f;
                EnablePrediction = true;
            }
            else if (ConfidenceScore > 0.5f)
            {
                PreferredBone = BoneTarget.Chest;
                AimSmoothing = 0.5f;
                EnablePrediction = true;
            }
            else
            {
                PreferredBone = BoneTarget.Stomach;
                AimSmoothing = 0.75f;
                EnablePrediction = false;
            }
        }
    }
}
