package be.pxl.emotion.services;

import org.springframework.jms.annotation.JmsListener;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.stereotype.Component;

import javax.jms.JMSException;
import javax.jms.TextMessage;

/**
 * Created by Dragonites on 20/11/2016.
 */
@Component
public class MessageConsumer {
    private JmsTemplate jmsTemplate;

    public JmsTemplate getJmsTemplate() {
        return jmsTemplate;
    }

    public void setJmsTemplate(JmsTemplate jmsTemplate) {
        this.jmsTemplate = jmsTemplate;
    }

    @JmsListener(destination = "ErrorQueue")
    public void recieveError() throws JMSException {
        Exception ex = (Exception) getJmsTemplate().receiveAndConvert();
    }

    @JmsListener(destination = "LogQueue")
    public void recieveMessage() throws JMSException {
        TextMessage tm = (TextMessage) getJmsTemplate().receive();
        String log = tm.getText();
    }
}
