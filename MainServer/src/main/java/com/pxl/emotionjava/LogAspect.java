package com.pxl.emotionjava;

import com.pxl.emotionjava.services.MessageProducer;
import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.annotation.AfterReturning;
import org.aspectj.lang.annotation.Aspect;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
@Aspect
public class LogAspect {

    @Autowired
    private MessageProducer messageProducer;

    @AfterReturning("execution(* com.pxl.emotionjava.controllers.*.get*(..))")
    private void getting(JoinPoint jp) {
        messageProducer.sendMessage(jp.getSignature().getName() + " was used to get data", jp.getSourceLocation().getWithinType());
    }

    @AfterReturning("execution(* com.pxl.emotionjava.controllers.*.add*(..))")
    private void adding(JoinPoint jp) {
        messageProducer.sendMessage(jp.getSignature().getName() + " was used to add " + jp.getArgs().hashCode() + " to database", jp.getSourceLocation().getWithinType());
    }

    @AfterReturning("execution(* com.pxl.emotionjava.controllers.*.update*(..))")
    private void updating(JoinPoint jp) {
        messageProducer.sendMessage(jp.getSignature().getName() + " was used to update " + jp.getArgs().hashCode() +" in database", jp.getSourceLocation().getWithinType());
    }

    @AfterReturning("execution(* com.pxl.emotionjava.controllers.*.delete*(..))")
    private void deleting(JoinPoint jp) {
        messageProducer.sendMessage(jp.getSignature().getName() + "was used to delete " + jp.getArgs().hashCode() +" in database", jp.getSourceLocation().getWithinType());
    }
}
