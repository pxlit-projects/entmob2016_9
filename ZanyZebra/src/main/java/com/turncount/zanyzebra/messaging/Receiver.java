package com.turncount.zanyzebra.messaging;

import org.springframework.jms.annotation.JmsListener;
import org.springframework.stereotype.Component;
import org.apache.log4j.Logger;
import org.apache.log4j.Logger;


@Component
public class Receiver {

    @JmsListener(destination = "log", containerFactory = "myFactory")
    public void receiveMessage(LogMessage message) {
        System.out.println(message);
    }

}
