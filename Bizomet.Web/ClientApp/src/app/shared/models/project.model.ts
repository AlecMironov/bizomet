export class ProjectModel {
    id: string;
    requestDate: Date;
    title: string;
    description: string;
    userPublicName: string;
    userPicture: string;
    userFirstName: string;
    userLastName: string;
    userEmail: string;
    interviewCondition: string;
    interviewConditionComment: string;
    interviewResult: string;
    interviewResultComment: string;
    wishContactedByMediaAssistant: boolean;
    mediaAssistantFinancialService: string;
    wishContactedByPromoter: boolean;
    promoterFinancialService: string;
    wishContactedByProducer: boolean;
    producerFinancialService: string;
    location: string;
    remoteLocation: boolean;
    dueDate?: Date;
    attachments?: any[];
    isArchived: boolean;
    isPublished: boolean;
}