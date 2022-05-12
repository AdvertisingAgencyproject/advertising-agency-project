import { Link } from 'react-router-dom';
import homeImg from './../assets/home.svg';

const Home = () => {
    return(
        <div className="home min-h-screen bg-cyan-900">
            <div className="grid grid-cols-2 container mx-auto items-center h-screen">
                <div className="grid grid-cols-1">
                    <div className="text-white text-5xl font-bold mb-5">Advertising agency</div>
                    <div className="text-white text-lg mb-5">
                        Advertising Agency is just like a tailor. 
                        We create the ads, plans how, when and where 
                        it should be delivered and hands it over to 
                        the client. We are mostly not 
                        dependent on any organizations.
                    </div>
                    <div className="grid grid-cols-2 w-1/3 text-center">
                        <Link to="/products" className="text-white bg-amber-500 px-3 py-2 rounded mr-2">Polygraphy</Link>
                        <Link to="/favors" className="text-white bg-indigo-800 px-3 py-2 rounded">Favors</Link>
                    </div>
                </div>
                <img alt="Image" src={homeImg}/>
            </div>
        </div>
    );
}

export default Home;