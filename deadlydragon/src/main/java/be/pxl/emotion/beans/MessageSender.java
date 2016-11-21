package be.pxl.emotion.beans;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.jms.core.MessageCreator;
import org.springframework.stereotype.Component;

import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.Session;

/**
 * Created by Dragonites on 20/11/2016.
 */

@Component
public class MessageSender {
    @Autowired
    private JmsTemplate jmsTemplate;

    public void sendLog(final String log)
    {
        jmsTemplate.send("LogQueue", new MessageCreator() {
            @Override
            public Message createMessage(Session session) throws JMSException {
                return session.createTextMessage(log);
            }
        });
    }
}
