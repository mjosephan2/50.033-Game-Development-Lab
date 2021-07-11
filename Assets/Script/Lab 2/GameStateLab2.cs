public static class GameStateLab2
{
    private static int restartCount = 0;
    public static int getRestartCount()
    {
        return restartCount;
    }

    public static void incrementRestart()
    {
        restartCount++;
    }
}