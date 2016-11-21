package be.pxl.emotion.beans;

import org.apache.log4j.Logger;
import org.springframework.jms.annotation.JmsListener;
import org.springframework.messaging.Message;
import org.springframework.stereotype.Component;

import javax.jms.JMSException;
import javax.jms.TextMessage;

/**
 * Created by Dragonites on 20/11/2016.
 */
@Component
public class MessageReceiver {
    @JmsListener(destination="LogQueue")
    public void onMessage(Message msg) throws JMSException {
        Logger logger = Logger.getLogger(MessageReceiver.class);
        if (msg instanceof TextMessage) {
            String log = ((TextMessage) msg).getText();
            logger.info(log);
        }
    }
}
