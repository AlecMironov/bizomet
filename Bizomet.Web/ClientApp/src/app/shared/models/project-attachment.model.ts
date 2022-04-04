export class ProjectAttachmentModel {
    id: string;
    projectId: string;
    title: string;
    fileName: string;
    fileType: string;
    size: number;
    binaryContent: string | ArrayBuffer;
    thumbnail?: string;
}