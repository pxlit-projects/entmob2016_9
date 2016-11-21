package be.pxl.emotion.beans;

import org.springframework.jms.annotation.JmsListener;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.stereotype.Component;

import javax.jms.JMSException;

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

    @JmsListener(destination = "LogQueue")
    public void receiveMessage() throws JMSException {
        Exception ex = (Exception) getJmsTemplate().receiveAndConvert();

    }
}
