package com.turncount.zanyzebra.services;

import org.aspectj.lang.annotation.Aspect;
import org.aspectj.lang.annotation.Around;

import java.util.Calendar;

import org.aspectj.lang.ProceedingJoinPoint;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import com.turncount.zanyzebra.entities.User;
import com.turncount.zanyzebra.repositories.UserRepository;

@Aspect
@Component
public class UserDataServiceAspect {
	@Autowired
	private UserRepository repo;

	@Around("execution(* com.turncount.zanyzebra.services.UserDataService.addOrUpdateUser (com.turncount.zanyzebra.entities.User)) && args(user)")
	public Object aroundUserCreation(ProceedingJoinPoint proceedingJoinPoint, User user) throws Throwable {
		
		if (!repo.exists(user.getUserId())){
            Calendar cal = Calendar.getInstance();       
            user.setJoinedOn(cal.getTime().toString());          
        }
		
		String result = (String) proceedingJoinPoint.proceed(new Object[] {user});
        return result;
	}
}
