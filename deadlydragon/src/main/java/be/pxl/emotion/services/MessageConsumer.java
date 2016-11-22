package be.pxl.emotion.services;

import org.apache.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
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
    @Autowired
    private JmsTemplate jmsTemplate;
    
    final static Logger logger = Logger.getLogger(MessageConsumer.class);

    public JmsTemplate getJmsTemplate() {
        return jmsTemplate;
    }

    public void setJmsTemplate(JmsTemplate jmsTemplate) {
        this.jmsTemplate = jmsTemplate;
    }

    @JmsListener(destination = "ErrorQueue")
    public void recieveError() throws JMSException {
        Exception ex = (Exception) getJmsTemplate().receiveAndConvert();
        logger.error("Sorry, something went wrong",ex);
        
    }

    @JmsListener(destination = "LogQueue")
    public void recieveMessage() throws JMSException {
        TextMessage tm = (TextMessage) getJmsTemplate().receive();
        String log = tm.getText();
        logger.info(log);
    }
}
