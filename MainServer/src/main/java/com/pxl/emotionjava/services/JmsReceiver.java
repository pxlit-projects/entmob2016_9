package com.pxl.emotionjava.services;

import org.apache.log4j.Logger;
import org.springframework.jms.annotation.JmsListener;
import org.springframework.stereotype.Service;

import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.TextMessage;

@Service
public class JmsReceiver {

    private Logger logger;

    @JmsListener(destination = "ErrorQueue")
    public void receiveError(Message msg) throws JMSException {
        if (msg instanceof TextMessage) {
            String text = ((TextMessage) msg).getText();
            logger = Logger.getLogger(Exception.class);
            logger.error(text);
        }
    }

    @JmsListener(destination = "LogQueue")
    public void receiveMessage(Message msg) throws JMSException, ClassNotFoundException {
        String className = msg.getStringProperty("origin");
        String message = msg.getStringProperty("content");
        logger = Logger.getLogger(Class.forName(className));
        logger.info(message);
    }
}
