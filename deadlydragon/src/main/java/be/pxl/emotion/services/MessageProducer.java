package be.pxl.emotion.services;

import org.springframework.jms.core.JmsTemplate;
import org.springframework.jms.core.MessageCreator;
import org.springframework.stereotype.Component;

import javax.jms.JMSException;
import javax.jms.ObjectMessage;
import javax.jms.Session;
import javax.jms.TextMessage;

/**
 * Created by Dragonites on 20/11/2016.
 */

@Component
public class MessageProducer {
    private JmsTemplate jmsTemplate;

    public JmsTemplate getJmsTemplate() {
        return jmsTemplate;
    }

    public void setJmsTemplate(JmsTemplate jmsTemplate) {
        this.jmsTemplate = jmsTemplate;
    }

    public void sendError(final Exception exception) {
        getJmsTemplate().send("ErrorQueue", new MessageCreator() {
            @Override
            public ObjectMessage createMessage(Session session) throws JMSException {
                return session.createObjectMessage(exception);
            }
        });
    }

    public void sendMessage(final String message) {
        getJmsTemplate().send("LogQueue", new MessageCreator() {
            @Override
            public TextMessage createMessage(Session session) throws JMSException {
                return session.createTextMessage(message);
            }
        });
    }
}
