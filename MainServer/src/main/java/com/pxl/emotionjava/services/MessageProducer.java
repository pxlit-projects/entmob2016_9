package com.pxl.emotionjava.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.jms.core.MessageCreator;
import org.springframework.stereotype.Component;

import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.Session;
import javax.jms.TextMessage;


@Component
public class MessageProducer {

    @Autowired
    private JmsTemplate jmsTemplate;

    public JmsTemplate getJmsTemplate() {
        return jmsTemplate;
    }

    public void sendError(final Exception exception) {
        getJmsTemplate().send("ErrorQueue", new MessageCreator() {
            public TextMessage createMessage(Session session) throws JMSException {
                return session.createTextMessage(exception.toString());
            }
        });
    }

    public void sendMessage(final String message, final Class clazz) {
        getJmsTemplate().send("LogQueue", new MessageCreator() {
            public Message createMessage(Session session) throws JMSException {
                Message message1 =  session.createMessage();
                message1.setStringProperty("origin", clazz.getName());
                message1.setStringProperty("content", message);
                return message1;
            }
        });
    }
}
