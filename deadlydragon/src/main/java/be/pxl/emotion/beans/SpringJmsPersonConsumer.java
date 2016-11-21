package be.pxl.emotion.beans;

import org.springframework.jms.core.JmsTemplate;

import javax.jms.JMSException;

public class SpringJmsPersonConsumer {
	
	private JmsTemplate jmsTemplate;

	public JmsTemplate getJmsTemplate() {
		return jmsTemplate;
	}

	public void setJmsTemplate(JmsTemplate jmsTemplate) {
		this.jmsTemplate = jmsTemplate;
	}
	
	public Person receiveMessage() throws JMSException {
		Person person = (Person) getJmsTemplate().receiveAndConvert();
		return person;	
	}
}
