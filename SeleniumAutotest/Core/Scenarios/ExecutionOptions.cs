namespace SeleniumAutotest.Core.Scenarios
{
    public class ExecutionOptions
    {
        /// <summary>
        /// Указывает на то, сколько итераций будет выполнено в потоке до делэя
        /// </summary>
        public const int PackageSize = 10;

        public  Guid TestId { get; set; }
        public Guid ScenarioId { get; set; }
        public int Iterations { get; set; }
        public bool Parallel { get; set; }
        public string[] Browsers { get; set; }


        public bool IsValid(out string message)
        {
            message = "";
            var result = true;

            if (Browsers == null || Browsers.Length == 0)
            {
                message += "Выберите как минимум 1 браузер <br>";
                result = false;
            }

            if (Iterations <= 0)
            {
                message += "Укажите как минимум 1 итерацию <br>";
                result = false;
            }

            return result;
        }
    }
}
