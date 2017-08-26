package com.pxl.emotionjava.services;

import org.aspectj.lang.annotation.AfterThrowing;
import org.aspectj.lang.annotation.Aspect;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
@Aspect
public class ErrorAspect {

    @Autowired
    private MessageProducer messageProducer;

    @AfterThrowing(value = "execution(* *.*(..))", throwing = "ex")
    private void error(Exception ex) {
        messageProducer.sendError(ex);
    }
}
