package com.turncount.zanyzebra.messaging;

import java.text.SimpleDateFormat;
import java.util.Calendar;

public class LogMessage {
	private String message;
	private boolean isError;
	private String caller;

	public LogMessage() {}
	
	public LogMessage(String message, String caller) {
		this.message = message;
		this.caller = caller;
		isError = false;
	}
	
	public LogMessage(String message, String caller, boolean isError) {
		this.message = message;
		this.caller = caller;
		this.isError = isError;
	}

	public String getMessage() {
		return message;
	}
	
	public boolean isError() {
		return isError;
	}
	
	public String getCaller() {
		return caller;
	}
	
	@Override
    public String toString() {
		Calendar cal = Calendar.getInstance();
		SimpleDateFormat format1 = new SimpleDateFormat("yyyy-MM-dd hh:mm:ssss");
			
        return String.format("%s  %s --- [%s] : %s", format1.format(cal.getTime()) , isError() ? "ERROR" : "LOG", getCaller(), getMessage());
    }
}
