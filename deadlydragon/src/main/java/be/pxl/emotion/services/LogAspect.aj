package be.pxl.emotion.services;

import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.annotation.AfterReturning;
import org.aspectj.lang.annotation.Pointcut;

/**
 * Created by Dragonites on 22/11/2016.
 */
public aspect LogAspect {
    @AfterReturning("execution(* *Controller.add*(..))")
    private void adding(JoinPoint jp) {
        new MessageProducer().sendMessage(jp.getKind() + " was used to add " + jp.getArgs().hashCode() + " to database");
    }
    @AfterReturning("execution(* *Controller.update*(..))")
    private void updating(JoinPoint jp) {
        new MessageProducer().sendMessage(jp.getKind() + " was used to update " + jp.getArgs().hashCode() +" in database");
    }
    @AfterReturning("execution(* *Controller.delete*(..))")
    private void deleting(JoinPoint jp) {
        new MessageProducer().sendMessage(jp.getKind() + "was used to delete " + jp.getArgs().hashCode() +" in database");
    }
}
