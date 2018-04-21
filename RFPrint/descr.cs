	public partial class Service : ServiceBase
	{
		OleDbConnection conn;
		protected override void OnStart(string[] args)
		{            
			conn = new OleDbConnection();
			conn.Open();
			timer1 = new Timer();
			this.timer1.Interval = 3000;
			this.timer1.Elapsed += new ElapsedEventHandler(this.timer1_Tick);
			timer1.Enabled = true;
		}
		private void timer1_Tick(object sender, EventArgs e)
		{
			OleDbCommand command = new OleDbCommand("SOME SQL QUERY", conn);
			OleDbDataReader reader = command.ExecuteReader();//запрос занимает времени ~50мс
			if (reader.HasRows)
			{
				while (reader.Read()) //на практике порядка 6 итераций
				{
					List<object> obj = new List<object>();
					obj.Add(conn);
					obj.Add(reader["Column1"].ToString());
					ThreadPool.QueueUserWorkItem(new WaitCallback((s) => { BackgroundJob(obj); }));
				}
			}
		}
		private static void BackgroundJob(object obj)
		{
			List<object> args = (List<object>)obj;
			OleDbConnection conn = (OleDbConnection)args[0];
			string data = (string)args[1];
			//...do main job
			OleDbCommand command = new OleDbCommand("SOME SQL QUERY", conn);//запрос занимает времени ~20мс
			command.ExecuteNonQuery();
			Thread.Sleep(1);
		}
	}