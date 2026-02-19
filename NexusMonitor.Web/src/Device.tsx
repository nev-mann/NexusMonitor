import { useParams } from 'react-router-dom';


function Device() {
    const { deviceId } = useParams<{ deviceId?: string }>();

    return (
        <div>
            <p>ID: {deviceId}</p>
        </div>
    );
};

export default Device