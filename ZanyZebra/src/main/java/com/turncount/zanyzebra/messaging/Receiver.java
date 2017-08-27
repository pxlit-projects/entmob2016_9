package com.turncount.zanyzebra.messaging;

import org.springframework.jms.annotation.JmsListener;
import org.springframework.stereotype.Component;

@Component
public class Receiver {

    @JmsListener(destination = "log", containerFactory = "myFactory")
    public void receiveMessage(LogMessage message) {
        System.out.println(message);
    }

}
