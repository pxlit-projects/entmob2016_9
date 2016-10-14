package be.pxl.emotion.main;

import java.util.Timer;
import java.util.TimerTask;

import javax.servlet.*;
import javax.servlet.http.HttpServlet;
 
@SuppressWarnings("serial")
public class StartUp extends HttpServlet
{	
	
	private static final int TIMER_INTERVAL_IN_MILLISECONDS = 50*1000;
	
    public void init() throws ServletException
    {
    	System.out.println("started");
    	
    	new PropertyLoader().Load();
    	
		Timer timer = new Timer();

		timer.schedule( new TimerTask() {
		    public void run() {
		    	//ParkingService.UpdateParkingData();
		    }
		 }, 0, TIMER_INTERVAL_IN_MILLISECONDS);
    }
}
